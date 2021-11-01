namespace Service.Converter.WebApi.Domain.Entities
{
    using System;
    using FluentValidation;
    using FluentValidation.Results;

    public abstract class Entity
  {
    public Entity()
    {
      Id = Guid.NewGuid();
    }
    
    public Guid Id { get; private set; }

    public ValidationResult ValidationResult { get; private set; }
    public bool IsValid { get; private set; }

    public bool Validate<TEntity>(TEntity model, AbstractValidator<TEntity> validator)
    {
      ValidationResult = validator.Validate(model);
      return IsValid = ValidationResult.IsValid;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is Entity)) return false;

      var entity = obj as Entity;

      if (this.GetType() != entity.GetType()) return false;
      if (!(entity is null) && ReferenceEquals(this, entity)) return true;

      return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity left, Entity right)
    {
      if (left is null && right is null) return true;
      if (left is null || right is null) return false;

      return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right) => !(left == right);

    public override int GetHashCode() => (GetType().GetHashCode() * 1111) + Id.GetHashCode();

    public Entity GetCopy() => (Entity)this.MemberwiseClone();
  }
}
