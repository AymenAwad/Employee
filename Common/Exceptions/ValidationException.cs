

namespace Shared.Resources

{
    using FluentValidation.Results;
    using Localization.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class ValidationException : Exception
    {

        public ValidationException() : base(SharedResource.ValidationExceptionMessage)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
