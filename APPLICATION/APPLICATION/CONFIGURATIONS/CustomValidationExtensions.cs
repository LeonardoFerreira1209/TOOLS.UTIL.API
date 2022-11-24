﻿using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using FluentValidation.Results;

namespace APPLICATION.APPLICATION.CONFIGURATIONS
{
    /// <summary>
    /// Extensão para o Validation Customizados.
    /// </summary>
    public static class CustomValidationExtensions
    {
        public static ApiResponse<object> CarregarErrosValidator(this ValidationResult validationResult)
        {
            var _notificacoes = new List<DadosNotificacao>();

            foreach (var erro in validationResult.Errors) _notificacoes.Add(new DadosNotificacao(erro.ErrorMessage));

            return new ApiResponse<object>
            {
                Sucesso = false,
                Notificacoes = _notificacoes.ToList()
            };
        }
    }
}
