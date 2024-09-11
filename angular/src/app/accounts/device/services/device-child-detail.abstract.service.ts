import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import type { DeviceDto } from '../../../proxy/devices/models';
import { ApiResponse, DeviceService } from '../../../proxy/devices/device.service';

export abstract class AbstractDeviceDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(DeviceService);
  public readonly list = inject(ListService);
  public errorMessage: string = '';

  accountId: string;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest(): Observable<ApiResponse<DeviceDto>> {

    return this.proxyService.create(this.form!.value);
  }

  protected updateRequest() {
    return this.proxyService.update(this.selected.id, this.form!.value);
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
      connection: [{ value: connection ?? 'false', disabled: true }, [Validators.required]],
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
    if (this.form!.invalid) return;
    this.isBusy = true;

    if (!this.selected) {
      const request$: Observable<ApiResponse<DeviceDto>> = this.createRequest().pipe(
        finalize(() => (this.isBusy = false))
      );

      request$.subscribe({
        next: (response: ApiResponse<DeviceDto>) => {
          if (response.success) {
            this.list.get();
            this.isBusy = false;
            this.hideForm();
          } else {
            console.error(response.message);
            this.errorMessage = JSON.parse(response.message).message;
          }
        },
        error: (err: any) => { 
          console.error('Request failed', err);
          // Keep the modal open and display the error message
        } 
      });
    }
    else {

    }
  }

  changeVisible(isVisible: boolean) {
    this.isVisible = isVisible;
  } 
}
