import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'iotms.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'iotms.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
