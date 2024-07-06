using iotms.Samples;
using Xunit;

namespace iotms.EntityFrameworkCore.Applications;

[Collection(iotmsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<iotmsEntityFrameworkCoreTestModule>
{

}
