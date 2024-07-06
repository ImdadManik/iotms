import { inject, ChangeDetectorRef } from '@angular/core';
import { filter, switchMap } from 'rxjs/operators';
import { ABP, ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import type { GetDeviceListInput, DeviceDto } from '../../../proxy/devices/models';
import { DeviceService } from '../../../proxy/devices/device.service';

export abstract class AbstractDeviceViewService {
  protected readonly cdr = inject(ChangeDetectorRef);
  protected readonly proxyService = inject(DeviceService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  data: PagedResultDto<DeviceDto> = {
    items: [],
    totalCount: 0,
  };

  delete(record: DeviceDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.id))
      )
      .subscribe(this.list.get);
  }

  hookToQuery(accountId: string) {
    const getData = (query: ABP.PageQueryParams) =>
      this.proxyService.getListByAccountId({
        ...(query as GetDeviceListInput),
        accountId,
      });

    const setData = (list: PagedResultDto<DeviceDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(list => {
      setData(list);
      this.cdr.markForCheck();
    });
  }
}
