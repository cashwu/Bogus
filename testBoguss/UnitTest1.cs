using System;
using System.Linq;
using Bogus;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Xunit;
using Xunit.Abstractions;

namespace testBoguss
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _outputHelper;

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public void Test1()
        {
            var userGenerator = new Faker<User>("zh_TW")
                                .RuleFor(a => a.Id, b => b.IndexFaker)
                                .RuleFor(a => a.Guid, b => b.Random.Guid())
                                .RuleFor(a => a.FirstName, (b, a) => b.Name.FirstName())
                                .RuleFor(a => a.LastName, (b, a) => b.Name.LastName())
                                .RuleFor(a => a.Gender, b => b.Person.Gender)
                                .RuleFor(a => a.Email, b => b.Image.PicsumUrl())
                                .RuleFor(a => a.BirthDate, b => b.Date.Future());

            var users = userGenerator.GenerateForever().Take(10);

            foreach (var user in users)
            {
                _outputHelper.WriteLine(user.ToString());
            }

            var lorem = new Bogus.DataSets.Lorem("zh_TW");
            
            _outputHelper.WriteLine(lorem.Sentence(10));
        }
    }

    class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Bogus.DataSets.Name.Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Guid)}: {Guid}, {nameof(Gender)}: {Gender}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Email)}: {Email}, {nameof(BirthDate)}: {BirthDate}";
        }
    }
}