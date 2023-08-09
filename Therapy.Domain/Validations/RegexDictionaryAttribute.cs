using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Therapy.Domain.Validation {
  public class RegexDictionaryAttribute : ValidationAttribute
  {
    private readonly string[] _keys;

    public RegexDictionaryAttribute(params string[] keys)
    {
        _keys = keys;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
      foreach (var key in _keys)
        {
            if (RegexDictionary.Regexes.ContainsKey(key))
            {
                var regex = new Regex(RegexDictionary.Regexes[key]);

                if (regex.IsMatch((string)value))
                {
                    return ValidationResult.Success;
                }
            }
        }

        return new ValidationResult($"Invalid format for {validationContext.MemberName}: {string.Join(", ", _keys)}");
    }
  }
}