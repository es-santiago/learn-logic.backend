#!/bin/bash

path="../"
className=$1

if [ -z "$className" ]; then
    echo "Por favor, forneça o nome da classe."
    exit 1
fi

# SERVICE

#CreateController
file=$path"2-Service/LearnLogic.Services/Controllers/"$className"Controller.cs"

echo $file
tee $file > /dev/null << EOF
using LearnLogic.API.Services.Controllers;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;

namespace LearnLogic.API.Controllers
{
    public class ${className}Controller : BaseController<${className}ViewModel, ${className}DTO, I${className}Application>
    {
        public ${className}Controller(I${className}Application service) : base(service) { }
    }
}
EOF

# APPLICATION

#CreateApplication
file=$path"3-Application/LearnLogic.Application/Services/Application/"$className"Application.cs"

echo $file
tee $file > /dev/null << EOF
using AutoMapper;
using LearnLogic.Application.Commands;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Domain.ViewModels;
using NetDevPack.Mediator;

namespace LearnLogic.Application.Services.Application
{
    public class ${className}Application : BaseApplication<${className}ViewModel, ${className}DTO, I${className}Repository, RegisterNew${className}Command, Update${className}Command, ChangeStatus${className}Command, Delete${className}Command>, I${className}Application
    {
        public ${className}Application(I${className}Repository repository, IMapper mapper, IMediatorHandler mediator) : base(repository, mapper, mediator) { }
    }
}
EOF

#CreateCommandHandler
file=$path"3-Application/LearnLogic.Application/Commands/"$className"CommandHandler.cs"

echo $file
tee $file > /dev/null << EOF
using AutoMapper;
using LearnLogic.Domain.Interfaces.Repository;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using LearnLogic.Domain.Interfaces.Application;

namespace LearnLogic.Application.Commands
{
    public class ${className}CommandHandler : CommandHandler,
         IRequestHandler<RegisterNew${className}Command, ValidationResult>,
         IRequestHandler<Update${className}Command, ValidationResult>,
         IRequestHandler<ChangeStatus${className}Command, ValidationResult>,
         IRequestHandler<Delete${className}Command, ValidationResult>
    {
        private readonly I${className}Repository _repository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IUserClaimsAccessor _userClaimsAccessor;

        public ${className}CommandHandler(IMediatorHandler mediator, IUserClaimsAccessor userClaimsAccessor, I${className}Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _userClaimsAccessor = userClaimsAccessor;
        }

        public async Task<ValidationResult> Handle(RegisterNew${className}Command message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(Update${className}Command message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(ChangeStatus${className}Command message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(Delete${className}Command message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
EOF

#CreateCommand
file=$path"3-Application/LearnLogic.Application/Commands/"$className"Command.cs"

echo $file
tee $file > /dev/null << EOF
using LearnLogic.Domain.ViewModels;
using NetDevPack.Messaging;

namespace LearnLogic.Application.Commands
{
    public abstract class ${className}Command : Command
    {
        public ${className}ViewModel Obj { get; protected set; }
    }

    public class RegisterNew${className}Command : ${className}Command
    {
        public RegisterNew${className}Command(${className}ViewModel ${className}Commands) => Obj = ${className}Commands;

        public override bool IsValid() =>
            (ValidationResult = new RegisterNew${className}CommandValidation().Validate(this)).IsValid;
    }

    public class Update${className}Command : ${className}Command
    {
        public Update${className}Command(${className}ViewModel ${className}Commands) => Obj = ${className}Commands;

        public override bool IsValid() =>
            (ValidationResult = new Update${className}CommandValidation().Validate(this)).IsValid;
    }

    public class ChangeStatus${className}Command : ${className}Command
    {
        public Guid Id { get; protected set; }
        public ChangeStatus${className}Command(Guid id) => Id = id;

        public override bool IsValid() => Id != Guid.Empty;
    }

    public class Delete${className}Command : ${className}Command
    {
        public Guid Id { get; protected set; }
        public Delete${className}Command(Guid id) => Id = id;

        public override bool IsValid() => Id != Guid.Empty;
    }
}
EOF

#CreateCommandValidation
file=$path"3-Application/LearnLogic.Application/Commands/"$className"CommandValidation.cs"

echo $file
tee $file > /dev/null << EOF
using FluentValidation;

namespace LearnLogic.Application.Commands
{
    public class ${className}CommandValidation { }

    public class RegisterNew${className}CommandValidation : ${className}Validation<RegisterNew${className}Command>
    {
        public RegisterNew${className}CommandValidation() { }
    }

    public class Update${className}CommandValidation : ${className}Validation<Update${className}Command>
    {
        public Update${className}CommandValidation() { }
    }

    public abstract class ${className}Validation<T> : AbstractValidator<T> where T : ${className}Command { }
}
EOF

#CreateInterfaceApplication
file=$path"4-Domain/LearnLogic.Domain/Interfaces/Application/"I$className"Application.cs"

echo $file
tee $file > /dev/null << EOF
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface I${className}Application : IBaseApplication<${className}ViewModel, ${className}DTO>{ }
}
EOF

#CreateInterfaceRepository
file=$path"4-Domain/LearnLogic.Domain/Interfaces/Repository/"I$className"Repository.cs"

echo $file
tee $file > /dev/null << EOF
using LearnLogic.Domain.DTO;
using System;

namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface I${className}Repository : IBaseRepository<${className}DTO> { }
}
EOF

#CreateViewModel
file=$path"4-Domain/LearnLogic.Domain/ViewModels/"$className"ViewModel.cs"

echo $file
tee $file > /dev/null << EOF
namespace LearnLogic.Domain.ViewModels
{
    public class ${className}ViewModel : BaseViewModel
    {
    }
}
EOF

#CreateDTO
file=$path"4-Domain/LearnLogic.Domain/DTO/"$className"DTO.cs"

echo $file
tee $file > /dev/null << EOF
namespace LearnLogic.Domain.DTO
{
    public class ${className}DTO : BaseDTO
    {
    }
}
EOF

#CreateEntity
file=$path"5-Infra/5.1-Data/LearnLogic.Infra.Data/Entities/"$className"Entity.cs"

echo $file
tee $file > /dev/null << EOF
namespace LearnLogic.Infra.Data.Entities
{
    public class ${className}Entity : BaseEntity
    {
    }
}
EOF

#CreateConfiguration

file=$path"5-Infra/5.1-Data/LearnLogic.Infra.Data/Configuration/"$className"Config.cs"

echo $file
tee $file > /dev/null << EOF
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LearnLogic.Infra.Data.Entities;

namespace LearnLogic.Infra.Data.Configuration
{
    public class ${className}Config : BaseConfig<${className}Entity>
    {
        public ${className}Config() : base("${className}") { }

        public override void Configure(EntityTypeBuilder<${className}Entity> builder)
        {
            base.Configure(builder);
        }
    }
}
EOF

#CreateRepository
file=$path"5-Infra/5.1-Data/LearnLogic.Infra.Data/Repositories/"$className"Repository.cs"

echo $file
tee $file > /dev/null << EOF
using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Infra.Data.Entities;
using LearnLogic.Infra.Data.Repositories;

namespace Mastership.Infra.Data.Repositories.Core
{
    public class ${className}Repository : BaseRepository<${className}Entity, ${className}DTO>, I${className}Repository
    {
        public ${className}Repository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
    }
}
EOF

# UpdateAutoMapper

file=$path"5-Infra/5.2-CrossCutting/LearnLogic.Infra.CrossCutting.IoC/AutoMapper/AutoMapperProfile.cs"

# Insert new mapping in MapperViewModelDTO method before the closing brace
sed -i "/private void MapperViewModelDTO()/,/\}/ {
    /private void MapperViewModelDTO()/!b
    :a
    /\}/ {
        i\\            CreateMap<${className}ViewModel, ${className}DTO>().ReverseMap();
        b
    }
    n
    ba
}" "$file"

# Insert new mapping in MapperDTOEntity method before the closing brace
sed -i "/private void MapperDTOEntity()/,/\}/ {
    /private void MapperDTOEntity()/!b
    :a
    /\}/ {
        i\\            CreateMap<${className}DTO, ${className}Entity>().ReverseMap();
        b
    }
    n
    ba
}" "$file"