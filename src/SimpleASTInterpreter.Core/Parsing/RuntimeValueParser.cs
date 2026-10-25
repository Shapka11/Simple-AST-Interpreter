using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using SimpleASTInterpreter.Core.Values;

namespace SimpleASTInterpreter.Core.Parsing;

public sealed class RuntimeValueParser
{
    private readonly string _text;
    private int _position;

    private RuntimeValueParser(string text)
    {
        _text = text;
    }

    public static RuntimeValue Parse(string text)
    {
        var parser = new RuntimeValueParser(text);
        RuntimeValue value = parser.ParseValue(0);
        parser.SkipWhitespace();
        if (parser._position != text.Length)
        {
            throw new FormatException("Unexpected text after the input value.");
        }

        return value;
    }

    private RuntimeValue ParseValue(int depth)
    {
        if (depth >= 512)
        {
            throw new FormatException("The input value is nested too deeply.");
        }

        SkipWhitespace();
        if (_position == _text.Length)
        {
            throw new FormatException("Expected an input value.");
        }

        char character = _text[_position];
        if (character == '"')
        {
            return new StringValue(ParseString());
        }

        if (character == '\'')
        {
            _position++;
            int start = _position;
            while (_position < _text.Length && _text[_position] != '\'')
            {
                if (_text[_position] == '\\')
                {
                    _position++;
                }

                _position++;
            }

            string literal = _text[start..Math.Min(_position, _text.Length)];
            Expect('\'');
            string decoded = literal switch
            {
                "\\'" => "'",
                "\\\\" => "\\",
                "\\n" => "\n",
                "\\t" => "\t",
                "\\r" => "\r",
                "\\b" => "\b",
                _ => literal
            };
            byte[] bytes = Encoding.UTF8.GetBytes(decoded);
            if (bytes.Length != 1)
            {
                throw new FormatException("A character literal must contain one byte.");
            }

            return new NumberValue(bytes[0]);
        }

        if (character == '[')
        {
            _position++;
            return new ArrayValue(ParseElements(']', depth));
        }

        if (character == '{')
        {
            _position++;
            List<RuntimeValue> elements = ParseElements('}', depth);
            RuntimeValue list = new SexpValue("nil", Array.Empty<RuntimeValue>());
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                list = new SexpValue("cons", new[] { elements[i], list });
            }

            return list;
        }

        if (char.IsUpper(character))
        {
            int start = _position++;
            while (_position < _text.Length && (char.IsLetterOrDigit(_text[_position]) || _text[_position] is '_' or '\''))
            {
                _position++;
            }

            string name = _text[start.._position];
            return new SexpValue(name, Take('(') ? ParseElements(')', depth) : Array.Empty<RuntimeValue>());
        }

        int numberStart = _position;
        if (character is '-' or '+')
        {
            _position++;
        }

        while (_position < _text.Length && char.IsAsciiDigit(_text[_position]))
        {
            _position++;
        }

        return new NumberValue(long.Parse(_text[numberStart.._position], CultureInfo.InvariantCulture));
    }

    private List<RuntimeValue> ParseElements(char closing, int depth)
    {
        List<RuntimeValue> elements = new();
        if (Take(closing))
        {
            return elements;
        }

        do
        {
            elements.Add(ParseValue(depth + 1));
        } while (Take(','));

        Expect(closing);
        return elements;
    }

    private string ParseString()
    {
        int start = _position++;
        while (_position < _text.Length)
        {
            char character = _text[_position++];
            if (character == '"')
            {
                return JsonSerializer.Deserialize<string>(_text[start.._position])!;
            }

            if (character == '\\')
            {
                _position++;
            }
        }

        throw new FormatException("Unterminated string literal.");
    }

    private bool Take(char character)
    {
        SkipWhitespace();
        if (_position >= _text.Length || _text[_position] != character)
        {
            return false;
        }

        _position++;
        return true;
    }

    private void Expect(char character)
    {
        if (!Take(character))
        {
            throw new FormatException($"Expected '{character}' at position {_position}.");
        }
    }

    private void SkipWhitespace()
    {
        while (_position < _text.Length && char.IsWhiteSpace(_text[_position]))
        {
            _position++;
        }
    }
}
