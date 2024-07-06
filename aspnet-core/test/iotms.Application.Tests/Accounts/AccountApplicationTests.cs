using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace iotms.Accounts
{
    public abstract class AccountsAppServiceTests<TStartupModule> : iotmsApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IAccountsAppService _accountsAppService;
        private readonly IRepository<Account, Guid> _accountRepository;

        public AccountsAppServiceTests()
        {
            _accountsAppService = GetRequiredService<IAccountsAppService>();
            _accountRepository = GetRequiredService<IRepository<Account, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _accountsAppService.GetListAsync(new GetAccountsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2b0f6e66-f098-4d6a-bc59-9ec473da89f6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _accountsAppService.GetAsync(Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AccountCreateDto
            {
                Name = "9960d9b6ae5148d8bffc33bff29dfbf6e140a3bc7cd44712999991243d17a4772edd23d24d634f91aaf491f36bf4969bfbf93882fb2e4f0e88a935ed200063a865816b5ee73c428ea9f85d8ec3686ff6d621efab5b294a25afd626cd84701cd80160d98bed1b45ac9c023e351da6b98ab2c4775f949144efb5cf35690a",
                Location = "6c805a93776749d29b17749684d41aff9f7ed92d435b43198d508b1dccfbeca2458353cd34b14ca78806e8470cf04926e2fc6940f3014643be7038298faafad29dd03758df1b4c0c859f767555c40da84919b147c20941d99014a532745b45a3ec205a3b213a4350b93844c53a126de3a84391f04dab4c79b0c7ab72d0",
                Address = "8933dd8249e14ad597a96e46a2cbfbe6e3018429223747a99e1229bf2541ddabc8d963b67a3b457ea2829e8265dcd205f311cfe4f7fc483f9f58840ecf1f75ca85ab3498ea0a4607b622aab117f45f88d0953ee92f2d4af6b07eb62b19f7ffbaf2708c934a154ff1865068297c887aadb26da716e15f463394939cc086",
                Contact = "91732",
                Email = "1bd7d124@c6e4685b.com",
                Web = "c4facd92333d4eaa83b12bf7ad1a99ad05ddb7148a6b4836b7ad60eab15b24c2028cdae843974eba88784c784b414609742a0dd140194dc69947d9f5be0c214deebe8febc4984ddb93e2036ecdc150ec35b9d4d0f08343ea8e4dd69937894e07171df1c143a541419a0fd70c9d600e8da6a9809e0dc14b7e894875e358c503fc3989587d0a7042d1bb96f047cf46318f53b21d7f55fc4f34a411c19b092ec2ad5eb79647e9c0476ea573bbd403983fe9dc0fd6f9a58f4f0f8a39f9f04a4f1b288d0a3ad8da0e4f45aa0d2300e05cfa7a167b973c26e342aeb9bfe8a17932865c0e5a29a1e178486796d59f9480fa491986b8e9f89f4748bda172",
                Rooms = 1263132970,
                Status = true
            };

            // Act
            var serviceResult = await _accountsAppService.CreateAsync(input);

            // Assert
            var result = await _accountRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("9960d9b6ae5148d8bffc33bff29dfbf6e140a3bc7cd44712999991243d17a4772edd23d24d634f91aaf491f36bf4969bfbf93882fb2e4f0e88a935ed200063a865816b5ee73c428ea9f85d8ec3686ff6d621efab5b294a25afd626cd84701cd80160d98bed1b45ac9c023e351da6b98ab2c4775f949144efb5cf35690a");
            result.Location.ShouldBe("6c805a93776749d29b17749684d41aff9f7ed92d435b43198d508b1dccfbeca2458353cd34b14ca78806e8470cf04926e2fc6940f3014643be7038298faafad29dd03758df1b4c0c859f767555c40da84919b147c20941d99014a532745b45a3ec205a3b213a4350b93844c53a126de3a84391f04dab4c79b0c7ab72d0");
            result.Address.ShouldBe("8933dd8249e14ad597a96e46a2cbfbe6e3018429223747a99e1229bf2541ddabc8d963b67a3b457ea2829e8265dcd205f311cfe4f7fc483f9f58840ecf1f75ca85ab3498ea0a4607b622aab117f45f88d0953ee92f2d4af6b07eb62b19f7ffbaf2708c934a154ff1865068297c887aadb26da716e15f463394939cc086");
            result.Contact.ShouldBe("91732");
            result.Email.ShouldBe("1bd7d124@c6e4685b.com");
            result.Web.ShouldBe("c4facd92333d4eaa83b12bf7ad1a99ad05ddb7148a6b4836b7ad60eab15b24c2028cdae843974eba88784c784b414609742a0dd140194dc69947d9f5be0c214deebe8febc4984ddb93e2036ecdc150ec35b9d4d0f08343ea8e4dd69937894e07171df1c143a541419a0fd70c9d600e8da6a9809e0dc14b7e894875e358c503fc3989587d0a7042d1bb96f047cf46318f53b21d7f55fc4f34a411c19b092ec2ad5eb79647e9c0476ea573bbd403983fe9dc0fd6f9a58f4f0f8a39f9f04a4f1b288d0a3ad8da0e4f45aa0d2300e05cfa7a167b973c26e342aeb9bfe8a17932865c0e5a29a1e178486796d59f9480fa491986b8e9f89f4748bda172");
            result.Rooms.ShouldBe(1263132970);
            result.Status.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AccountUpdateDto()
            {
                Name = "0353fb8cea44438fa0fff16b7f03c887b115c226b6f444d3b413b0eb091a1edd7c872c9f9b8743f396e2dda5dc1fa3cbaffc8e6ad7db4b20a6b796b4caff1595b8122b0f7efd4f519dac102bf5fad34db6357324664f4f5f88434ffa5c4effcebd93154b40724d7fb771bf29931626a96876abc296bf4514bcd41be23a",
                Location = "9ac55b5bba5f40879f108536fad518fc63cf6b6391bf49c3a6658b12ce6bc4d579cd1a80f89f410a9f0505eb5e97805ece445969dc804132b54b547996b353e458c06313edab45369f6d911dedc69bf9800b6ad93adf4241a0878c09dee73842744ca72c74804554a1f9ad9b5ca9118e97c02231964a44d086ff8d6a91",
                Address = "004e7461da5046f99a8c32779584a7ca0b25c51e2df0485baf6fb13cffabfe4ad1ce0c2d0138400fb78ff98de7e743d068ab66ff14564825b1b2951658d7c6cd448469b56cca41bd8f942234d3c88875a4397b37589946669ac94848a082bb402cb1634ca8f2445cb68767d72243ec1fa83ba555433b434ebee4ad5680",
                Contact = "949167",
                Email = "73a48f49@99495acb.com",
                Web = "0780100baa504ba5844cc023d7464dced802d644405249ac8c2f9f3f5b1858adf989ea7985a84ce0b7bd1303ef8f4485340915193d8440f3970f03d2ebebc6cc40e572112e7a4305907fcad1630f0755778dae6d57d24b0ab13e9eee0bd8868feba13f38acf14c87ba158fbc465b0de070cf4045164843ab9aac841859804869aaee81119ff742548f89b415cff50653c31fa5bbbad94dfb8703b1f8ef060fb745f6085e707e4823ad30dfec98975282b8110ede57904923976372d81702094c267c77eb4ef446d29e45d8de8a651a3b608ff67ef6154eeebd8da5b4510303f73daaab26ac5b44e48989b072a76fc39a1b7009523adb434db33e",
                Rooms = 1687075620,
                Status = true
            };

            // Act
            var serviceResult = await _accountsAppService.UpdateAsync(Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"), input);

            // Assert
            var result = await _accountRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0353fb8cea44438fa0fff16b7f03c887b115c226b6f444d3b413b0eb091a1edd7c872c9f9b8743f396e2dda5dc1fa3cbaffc8e6ad7db4b20a6b796b4caff1595b8122b0f7efd4f519dac102bf5fad34db6357324664f4f5f88434ffa5c4effcebd93154b40724d7fb771bf29931626a96876abc296bf4514bcd41be23a");
            result.Location.ShouldBe("9ac55b5bba5f40879f108536fad518fc63cf6b6391bf49c3a6658b12ce6bc4d579cd1a80f89f410a9f0505eb5e97805ece445969dc804132b54b547996b353e458c06313edab45369f6d911dedc69bf9800b6ad93adf4241a0878c09dee73842744ca72c74804554a1f9ad9b5ca9118e97c02231964a44d086ff8d6a91");
            result.Address.ShouldBe("004e7461da5046f99a8c32779584a7ca0b25c51e2df0485baf6fb13cffabfe4ad1ce0c2d0138400fb78ff98de7e743d068ab66ff14564825b1b2951658d7c6cd448469b56cca41bd8f942234d3c88875a4397b37589946669ac94848a082bb402cb1634ca8f2445cb68767d72243ec1fa83ba555433b434ebee4ad5680");
            result.Contact.ShouldBe("949167");
            result.Email.ShouldBe("73a48f49@99495acb.com");
            result.Web.ShouldBe("0780100baa504ba5844cc023d7464dced802d644405249ac8c2f9f3f5b1858adf989ea7985a84ce0b7bd1303ef8f4485340915193d8440f3970f03d2ebebc6cc40e572112e7a4305907fcad1630f0755778dae6d57d24b0ab13e9eee0bd8868feba13f38acf14c87ba158fbc465b0de070cf4045164843ab9aac841859804869aaee81119ff742548f89b415cff50653c31fa5bbbad94dfb8703b1f8ef060fb745f6085e707e4823ad30dfec98975282b8110ede57904923976372d81702094c267c77eb4ef446d29e45d8de8a651a3b608ff67ef6154eeebd8da5b4510303f73daaab26ac5b44e48989b072a76fc39a1b7009523adb434db33e");
            result.Rooms.ShouldBe(1687075620);
            result.Status.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _accountsAppService.DeleteAsync(Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"));

            // Assert
            var result = await _accountRepository.FindAsync(c => c.Id == Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"));

            result.ShouldBeNull();
        }
    }
}