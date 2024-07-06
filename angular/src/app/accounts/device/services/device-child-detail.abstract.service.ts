import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';

import type { DeviceDto } from '../../../proxy/devices/models';
import { DeviceService } from '../../../proxy/devices/device.service';

export abstract class AbstractDeviceDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(DeviceService);
  public readonly list = inject(ListService);

  accountId: string;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest() {
    if (this.selected) {
      return this.proxyService.update(this.selected.id, this.form.value);
    }
    return this.proxyService.create(this.form.value);
  }

  buildForm() {
    const {
      name,
      status,
      temp,
      ldr,
      pir,
      door,
      minTempAlert,
      tempAlertFreq,
      minLDRAlert,
      ldrAlertFreq,
      connection,
    } = this.selected || {};

    this.form = this.fb.group({
      accountId: [this.accountId],
      name: [name ?? null, [Validators.required, Validators.maxLength(250)]],
      status: [status ?? 'true', [Validators.required]],
      temp: [temp ?? 'true', [Validators.required]],
      ldr: [ldr ?? 'true', [Validators.required]],
      pir: [pir ?? 'true', [Validators.required]],
      door: [door ?? 'true', [Validators.required]],
      minTempAlert: [
        minTempAlert ?? '20',
        [Validators.required, Validators.min(0), Validators.max(50)],
      ],
      tempAlertFreq: [
        tempAlertFreq ?? '30',
        [Validators.required, Validators.min(5), Validators.max(1440)],
      ],
      minLDRAlert: [
        minLDRAlert ?? null,
        [Validators.required, Validators.min(0), Validators.max(255)],
      ],
      ldrAlertFreq: [
        ldrAlertFreq ?? null,
        [Validators.required, Validators.min(5), Validators.max(1440)],
      ],
      connection: [connection ?? 'false', [Validators.required]],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: DeviceDto) {
    this.selected = record;
    this.showForm();
  }

  hideForm() {
    this.isVisible = false;
  }

  submitForm() {
    if (this.form.invalid) return;

    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm())
    );

    request.subscribe(this.list.get);
  }

  changeVisible(isVisible: boolean) {
    this.isVisible = isVisible;
  }
}
