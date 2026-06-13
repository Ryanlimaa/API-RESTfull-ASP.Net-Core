using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProdutos.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 60 caracteres!")]
    public string Nome { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero!")]
    public double Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo!")]
    public int Estoque { get; set; }
}