using iotms.Samples;
using Xunit;

namespace iotms.EntityFrameworkCore.Domains;

[Collection(iotmsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<iotmsEntityFrameworkCoreTestModule>
{

}
