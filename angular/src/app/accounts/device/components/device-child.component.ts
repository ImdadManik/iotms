import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { PageModule } from '@abp/ng.components/page';
import { ThemeSharedModule, DateAdapter } from '@abp/ng.theme.shared';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';

import { DeviceViewService } from '../services/device-child.service';
import { DeviceDetailViewService } from '../services/device-child-detail.service';
import { DeviceDetailModalComponent } from './device-child-detail.component';
import { AbstractDeviceComponent } from './device-child.abstract.component';

@Component({
  standalone: true,
  selector: 'app-device',
  imports: [
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    CoreModule,
    PageModule,
    ThemeSharedModule,
    DeviceDetailModalComponent,
    CommercialUiModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    { provide: NgbDateAdapter, useClass: DateAdapter },
    ListService,
    DeviceViewService,
    DeviceDetailViewService,
  ],
  templateUrl: './device-child.component.html',
})
export class DeviceComponent extends AbstractDeviceComponent {}
