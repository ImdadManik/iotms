using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using iotms.Accounts;
using iotms.EntityFrameworkCore;
using Xunit;

namespace iotms.EntityFrameworkCore.Domains.Accounts
{
    public class AccountRepositoryTests : iotmsEntityFrameworkCoreTestBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountRepositoryTests()
        {
            _accountRepository = GetRequiredService<IAccountRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _accountRepository.GetListAsync(
                    name: "5d633d6ce1b24d38a5d0420d95baa64ad8e1754b58a6468486ad772930659279e0f1fcfb02fd4bc5ad222c8c1d38faeabc43023d75ab4908a4194efe72d8e8b65df1647962fc4c3f815f4b00f86f8dfdffbcd0da4964450489ccdb208f614d2e4fce71c6bd0147e1bcd1f23b1e02cb5340daa7d1e9684807ad4b3fea60",
                    location: "fb3bbfea60dd4d9cabf2f520328e0c8289182e6db07045e18984f01b4f2bbcb347f7b8de60e940e5bc2e612940108e8d05fd15758ae045d18e737a5136990e41486a9032579843369f8f8750afd2574e32d6040d73074663bb2a89eddc12e23a323c088d1f324844880aa071c771ba5d8d6f501378304cb986e86b8167",
                    address: "d5b2d1f715ff4bbfa548bb8c462c0e01b9ae9dbbc0664dfc826d7339424345fec331dc728d5d42d1ad35eb200cf91a77fa6ab92f7f2e48a9b570e534d2c0da73dd8b5cf6418645a7880cb661cacb1aed89161029b1cb4b9ebf7257aff43561e340038930f6c84223a4a93c7c5ca2e37189832cd60d014635b167ff8db5",
                    contact: "44365",
                    email: "898f5cb5ecd14c3ea0a1312c5c3299e1@bb2230db56e34b148701c64362ccf636.com",
                    web: "8946b091f58343d59a218aaafc4b323f0954b78b630a406bafacf20dc377aa2671e01a07dc66433db76bd370c48bede3ef15d1ff2bd34e82ac23de0eaf9d81e9e838589c3f684074a2f7674bea2637d9f9260d8f02c54a28815324a471792ff8b3a00ba097f54ff48a298f4c441f761c2c2fee123ee54c6983c12c9d8cc1ee698c3d1fa46af04ef4a023d788eca3b541b02d1f2d6d0340d7a69e3201b068be01cd777e7cd3854305bb9dc9be3ed49d3f3dc5620924174fa388627a3ff62b81acbf0eaeab0c624eaa83399cbf9b1706dd8f4aa2e86e3545d380adca48648deed87df8cd77c7b04bdda34476bed70e36b00c0468415a454deea0a2",
                    status: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("25b37bd8-e2e3-45e0-b288-1b0eb96cd6a1"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _accountRepository.GetCountAsync(
                    name: "6eeb4e2dacd44a4fa9d0780730976d81cd76acab632b4fa9a98a76fbf2642d7f6bf5b5199568494092d784eb9b0ef960101fe00aad5740b99b7b76090e67cb500e55559f213449599e63ad2a42008e0aabbf1349fc6a4c04840c49acf6359e78ba2d1157f3274921bb2691aa72ce51d92949660a1a1741fab461b9abe4",
                    location: "256e6277d6fd42d2b54213a7521615dfc1fa8239ba874b12884314551f14c5b7b9785c6655af4fa6acc49482220757b3cffcbf7ed330474996e3dc93576005372c5a4339aa5f4ed6a68956ffe2e731a0646f5880b0ad4ea48baccf2837fcf85d18b652c4a3c14361ac5fd62b3230112a22f9fefae1704967b470364365",
                    address: "4b6f6ec85c8047da8f995f2bd30c20a894678b1484854f02a758006a2d7e78cc4a1efd57fb5b435090675c50d91bca2254ecd4c2d2c34daa9a66635fe105b5c93aba6b1ccf3a44c2ac9cb96651683f8026649545656e4c7283c4266f1da2868ed973f33e013f43e0bc0f31836310261fad2b8eec413645c08f4a73af5c",
                    contact: "234301",
                    email: "3d@b7.com",
                    web: "a95a9647599d4eac8bcecf8c4e675fc358a15400e54045e3a01e38c3e596ba445d1288e274324591bca9173e32acc9b6242f220fe5aa45b6afdb5f5db57cae87cb6c0362a8134e4d99789d68e3ec7d32e57ecac2af2946d882d629aa7f997fd080a152518fae4617bbe894ed1798af3200c81921b89240b9845cc92b2125d947d870a507f84b449fba5820c5436e29f30756b14b967046d3bcec639fa23f2e1a692ac4a46cd64f8c9219a2c853f0698e7a2850dacb574492bf27d60d7a48ddec282e35816ab64199a867fc8d838059c19fa57a3f43164f2f97eb1a7390040c6b1e499be054224d628ef578732c599aa744d731d1698e4a248c99",
                    status: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}