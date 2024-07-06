using Xunit;

namespace iotms.EntityFrameworkCore;

[CollectionDefinition(iotmsTestConsts.CollectionDefinitionName)]
public class iotmsEntityFrameworkCoreCollection : ICollectionFixture<iotmsEntityFrameworkCoreFixture>
{

}
