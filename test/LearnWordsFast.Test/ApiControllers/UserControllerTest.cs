using System;
using System.Collections.Generic;
using FluentAssertions;
using LearnWordsFast.ApiControllers;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using LearnWordsFast.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Moq;
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
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.Empty,
                TrainingLanguage = Guid.NewGuid()
            };

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns((Language)null);
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());

            var actual = await target.Create(request);
            actual.Should().BeOfType<BadRequestObjectResult>();

            languageRepository.Verify(x => x.Get(request.MainLanguage), Times.Once);
            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_TraininigLangNotSpecified_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.NewGuid(),
                TrainingLanguage = Guid.Empty
            };

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns((Language)null);
            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            languageRepository.Verify(x => x.Get(request.MainLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(request.TrainingLanguage), Times.Once);
            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AdditionalLanguageNotFound_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
            [NoAutoProperties] UserController target)
        {
            var request = new CreateUserViewModel
            {
                MainLanguage = Guid.NewGuid(),
                TrainingLanguage = Guid.NewGuid(),
                AdditionalLanguages = new List<Guid>
                {
                    Guid.Empty
                }
            };

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(Guid.Empty)).Returns((Language)null);

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            languageRepository.Verify(x => x.Get(request.MainLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(request.TrainingLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(Guid.Empty), Times.Once);
            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AdditionalLanguageSameAsMain_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
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

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(Guid.Empty)).Returns((Language)null);

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            languageRepository.Verify(x => x.Get(request.MainLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(request.TrainingLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(Guid.Empty), Times.Never);
            userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            signInManager.Verify(x => x.SignInAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory, AutoMoqData]
        public async void Create_AdditionalLanguageNotUnique_Error(
            [Frozen] Mock<ISignInManager> signInManager,
            [Frozen] Mock<IUserManager> userManager,
            [Frozen] Mock<ILanguageRepository> languageRepository,
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

            languageRepository.Setup(x => x.Get(request.MainLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(request.TrainingLanguage)).Returns(new Language());
            languageRepository.Setup(x => x.Get(Guid.Empty)).Returns((Language)null);

            var actual = await target.Create(request);

            actual.Should().BeOfType<BadRequestObjectResult>();

            languageRepository.Verify(x => x.Get(request.MainLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(request.TrainingLanguage), Times.Once);
            languageRepository.Verify(x => x.Get(additional), Times.Never);
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