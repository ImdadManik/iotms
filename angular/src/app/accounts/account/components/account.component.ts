import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbTimeAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbTimepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter, TimeAdapter } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';

import { AccountViewService } from '../services/account.service';
import { AccountDetailViewService } from '../services/account-detail.service';
import { AccountDetailModalComponent } from './account-detail.component';
import {
  AbstractAccountComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './account.abstract.component';

@Component({
  selector: 'app-account',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbTimepickerModule,
    NgbDropdownModule,

    NgxValidateCoreModule,

    PageModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    AccountDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    AccountViewService,
    AccountDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './account.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})
export class AccountComponent extends AbstractAccountComponent {}
