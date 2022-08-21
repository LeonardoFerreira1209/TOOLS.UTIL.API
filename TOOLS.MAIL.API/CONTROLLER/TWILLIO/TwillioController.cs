﻿using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.UTILS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace TOOLS.MAIL.API.CONTROLLER.TWILLIO
{
    [Route("api/[controller]")][ApiController]
    public class TwillioController : ControllerBase
    {
        private ITwillioService _twillioService;

        public TwillioController(ITwillioService twillioService)
        {
            _twillioService = twillioService;
        }

        /// <summary>
        /// Endpoint responsavel por enviar um sms.
        /// </summary>
        /// <param name="messageRequest"></param>
        /// <returns></returns>
        [HttpPost("sms/invite")][EnableCors("CorsPolicy")]
        [SwaggerOperation(Summary = "Enviar sms.", Description = "Método responsavel por enviar sms.")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<object>> SmsInvite(MessageRequest messageRequest)
        {
            using (LogContext.PushProperty("Controller", "ClaimController"))
            using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(messageRequest)))
            using (LogContext.PushProperty("Metodo", "AddClaim"))
            {
                return await Tracker.Time(() => _twillioService.Sms(messageRequest), "Enviar sms");
            }
        }

        /// <summary>
        /// Endpoint responsavel por receber o status do sms.
        /// </summary>
        /// <returns></returns>
        [HttpPost("sms/status")][EnableCors("CorsPolicy")]
        [SwaggerOperation(Summary = "Status do sms.", Description = "Método responsavel por receber o status do sms.")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task SmsStatus()
        {
            using (LogContext.PushProperty("Controller", "SmsController"))
            using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(null)))
            using (LogContext.PushProperty("Metodo", "StatusSms"))
            {

                var response = new StatusSmsRequest
                {
                    SmsSid = Request.Form["MessageSid"],
                    MessageStatus = Request.Form["MessageStatus"],
                    AccountSid = Request.Form["AccountSid"],
                    SmsStatus = Request.Form["SmsStatus"]
                };

                Log.Information($"Status sms {JsonConvert.SerializeObject(response)}");

                //return await Tracker.Time(() => smsService.Sms(request), "Status sms");
            }
        }

        /// <summary>
        /// Endpoint responsavel por enviar uma mensagem para o whatsapp.
        /// </summary>
        /// <returns></returns>
        [HttpPost("whatsapp/invite")][EnableCors("CorsPolicy")]
        [SwaggerOperation(Summary = "Enviar mensagens para o whatsapp.", Description = "Método responsavel por enviar mensagem para o whatsapp.")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<object>> WhatsappInvite(MessageRequest messageRequest)
        {
            using (LogContext.PushProperty("Controller", "WhatsappController"))
            using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(messageRequest)))
            using (LogContext.PushProperty("Metodo", "Whatsapp"))
            {
                return await Tracker.Time(() => _twillioService.Whatsapp(messageRequest), "Enviar mensagem para whatsapp");
            }
        }
    }
}