using System.ComponentModel.DataAnnotations;

namespace HaagsVertaler.Models
{
  public class TranslationViewModel
  {
    [MinLength(1)]
    [MaxLength(5000)]
    public string? Source { get; set; }
    public string? Result { get; set; }
  }
}