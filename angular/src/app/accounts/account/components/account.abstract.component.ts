import { Directive, OnInit, inject, ViewChild } from '@angular/core';

import {
  NgbNav,
  NgbNavItem,
  NgbNavLink,
  NgbNavContent,
  NgbNavOutlet,
} from '@ng-bootstrap/ng-bootstrap';
import { ListService, TrackByService } from '@abp/ng.core';

import type { AccountDto } from '../../../proxy/accounts/models';
import { AccountViewService } from '../services/account.service';
import { AccountDetailViewService } from '../services/account-detail.service';
import { DeviceComponent } from '../../device/components/device-child.component';

export const ChildTabDependencies = [NgbNav, NgbNavItem, NgbNavLink, NgbNavContent, NgbNavOutlet];

export const ChildComponentDependencies = [DeviceComponent];

@Directive({ standalone: true })
export abstract class AbstractAccountComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(AccountViewService);
  public readonly serviceDetail = inject(AccountDetailViewService);
  protected title = '::Accounts';

  @ViewChild('accountTable') table: any;

  ngOnInit() {
    this.service.hookToQuery();
  }

  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: AccountDto) {
    this.serviceDetail.update(record);
  }

  delete(record: AccountDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  toggleExpandRow(row) {
    this.table.rowDetail.toggleExpandRow(row);
  }
}
