using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("This is not Validator class");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {

            //Validatorun instance sini yarat
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            //Validatorun base class'nin generic type'ni mueyyen et
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //Metoda gelen parametrlerden yuxariya uygun gelenleri tap
            //invocation metoddur
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
