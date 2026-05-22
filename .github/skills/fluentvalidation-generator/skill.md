---
name: fluentvalidation-generator
description: Generates FluentValidation validators following project standards.
---

# FluentValidation Generator Skill

Use this skill when:
- Creating validators for request DTOs
- Adding validation rules for commands or models
- Generating consistent FluentValidation logic
- Applying project validation conventions

## Validation Standards

Follow these conventions:

1. Use AbstractValidator<T>
2. Group rules logically
3. Use descriptive error messages
4. Prefer cascade mode stop when appropriate
5. Use async validation only when database access is needed
6. Keep validators focused and readable

## Naming Conventions

| Type | Convention |
|---|---|
| Validator | `{ModelName}Validator` |
| File Name | `{ModelName}Validator.cs` |

## Required Patterns

### Required Field

```csharp
RuleFor(x => x.Name)
    .NotEmpty()
    .WithMessage("Name is required.");
```

### Max Length

```csharp
RuleFor(x => x.Name)
    .MaximumLength(100)
    .WithMessage("Name cannot exceed 100 characters.");
```

### Enum Validation

```csharp
RuleFor(x => x.Status)
    .IsInEnum();
```

### Collection Validation

```csharp
RuleForEach(x => x.Items)
    .SetValidator(new ItemValidator());
```

## Project-Specific Requirements

- Use `new FluentValidationResult()`
- Keep rules in constructor only
- Avoid duplicate database calls
- Prefer reusable helper methods for common rules

## Instructions

1. Analyze the DTO/request model
2. Generate validator using FluentValidation
3. Apply project naming conventions
4. Add meaningful validation messages
5. Save validator in the appropriate Validators folder

## Example Usage

Prompt examples:

- Generate FluentValidation validator for CreateUserRequest
- Create validator for UpdateOrganizationRequest
- Add validation rules for AssessmentRequest