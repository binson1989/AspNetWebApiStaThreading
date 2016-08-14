using Autofac;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApiStaThreading
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private IContainer _container;

        public AutofacValidatorFactory(IContainer container)
        {
            _container = container;
            Debug.WriteLine("XXX: ValidatorFactory instance created!!!");
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object validator;
            _container.TryResolve(validatorType, out validator);
            return validator as IValidator;
        }
    }
}
