using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckMobile.FiscalPrintingService.CommandProcessing;
using CheckMobile.FiscalPrintingService.Commands;
using CheckMobile.FiscalPrintingService.Transport;
using Microsoft.AspNetCore.Mvc;

namespace CheckMobile.FiscalPrintingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFiscalPrinterTransport _transceiver;
        public ValuesController(IFiscalPrinterTransport transceiver)
        {
            _transceiver = transceiver;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var command = new RequestStatusCommand();
            var commandPwd = new CommandPasswordWrapper(command, new byte[] { 0x00, 0x00 });
            var bufferCommand = new AddBufferCommand(commandPwd, 0x01, 0x01);

            var formatter = new CommandFormatter(bufferCommand);

            var buffer = formatter.CreatePackage();

            await _transceiver.SendPackage(buffer);

            return Ok();

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
