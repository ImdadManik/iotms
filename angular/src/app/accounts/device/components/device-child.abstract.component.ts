import { Directive, OnInit, inject, Input } from '@angular/core';
import { ListService, TrackByService } from '@abp/ng.core';

import type { DeviceDto } from '../../../proxy/devices/models';
import { DeviceViewService } from '../services/device-child.service';
import { DeviceDetailViewService } from '../services/device-child-detail.service';

@Directive({ standalone: true })
export abstract class AbstractDeviceComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(DeviceViewService);
  public readonly serviceDetail = inject(DeviceDetailViewService);

  @Input() title = '::Devices';

  @Input() accountId: string;

  ngOnInit() {
    this.serviceDetail.accountId = this.accountId;
    this.service.hookToQuery(this.accountId);
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: DeviceDto) {
    this.serviceDetail.update(record);
  }

  delete(record: DeviceDto) {
    this.service.delete(record);
  }
}
