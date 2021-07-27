using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeCycleIDController : ControllerBase
    {
        public readonly IExampleSingleton _exSingleton1;
        public readonly IExampleSingleton _exSingleton2;

        public readonly IExampleScoped _exScoped1;
        public readonly IExampleScoped _exScoped2;

        public readonly IExampleTransient _exTransient1;
        public readonly IExampleTransient _exTransient2;

        public LifeCycleIDController(IExampleSingleton exampleSingleton1,
                                       IExampleSingleton exampleSingleton2,
                                       IExampleScoped exampleScoped1,
                                       IExampleScoped exampleScoped2,
                                       IExampleTransient exampleTransient1,
                                       IExampleTransient exampleTransient2)
        {
            _exSingleton1 = exampleSingleton1;
            _exSingleton2 = exampleSingleton2;
            _exScoped1 = exampleScoped1;
            _exScoped2 = exampleScoped2;
            _exTransient1 = exampleTransient1;
            _exTransient2 = exampleTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exSingleton1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_exSingleton2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exScoped1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_exScoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exTransient1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_exTransient2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeneralExample
    {
        public Guid Id { get; }
    }

    public interface IExampleSingleton : IGeneralExample
    { }

    public interface IExampleScoped : IGeneralExample
    { }

    public interface IExampleTransient : IGeneralExample
    { }

    public class LifeCycleExample : IExampleSingleton, IExampleScoped, IExampleTransient
    {
        private readonly Guid _guid;

        public LifeCycleExample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }
}
