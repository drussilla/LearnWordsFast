using System;
using System.Collections.Generic;
using FluentAssertions;
using LearnWordsFast.API.Controllers;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.API.Services;
using LearnWordsFast.API.ViewModels.UserController;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Moq;
using NHibernate.Exceptions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace LearnWordsFast.Test.ApiControllers
{
    public class UserControllerTest
    {
        [Theory, AutoMoqData]
        public async void Create_MainAndTrainingLangTheSame_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var actual = await target.Create(new CreateUserViewModel
            {
                MainLanguage = Guid.Empty,
                TrainingLanguage = Guid.Empty
            });

            actual.Should().BeOfType<BadRequestObjectResult>();

            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_MainLangNotSpecified_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.Empty,
                TrainingLanguage = Guid.NewGuid()
            };

            userManager
                .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Throws(new GenericADOException("Test", new Exception { Data = { { "Code", "23503" } }}));

            var actual = await target.Create(request);
            actual.Should().BeOfType<BadRequestObjectResult>();

            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AdditionalLanguageSameAsMain_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [NoAutoProperties] UserController target)
        {
            var mainLang = Guid.NewGuid();
            var request = new CreateUserViewModel
            {
                MainLanguage = mainLang,
                TrainingLanguage = Guid.NewGuid(),
                AdditionalLanguages = new List<Guid>
                {
                    mainLang,
                    Guid.NewGuid()
                }
            };

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AdditionalLanguageNotUnique_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [NoAutoProperties] UserController target)
        {
            var additional = Guid.NewGuid();
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.NewGuid(),
                TrainingLanguage = Guid.NewGuid(),
                AdditionalLanguages = new List<Guid>
                {
                    additional,
                    additional
                }
            };

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_UserManagerIsNotSuccess_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.NewGuid(),
                TrainingLanguage = Guid.NewGuid()
            };

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AllIsOk_Ok(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.NewGuid(),
                TrainingLanguage = Guid.NewGuid(),
                Password = "password",
                Email = "test"
            };

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var actual = await target.Create(request);

            actual.Should().BeOfType<CreatedResult>();
            var createdResult = actual as CreatedResult;
            createdResult.Location.Should().StartWith("/api/user/");

            userManager.Verify(x => x.CreateAsync(It.Is<User>(u => u.Email == request.Email), request.Password), Times.Once);
            signInManager.Verify(x => x.SignInAsync(It.Is<User>(u => u.Email == request.Email)), Times.Once);
        }
    }
}