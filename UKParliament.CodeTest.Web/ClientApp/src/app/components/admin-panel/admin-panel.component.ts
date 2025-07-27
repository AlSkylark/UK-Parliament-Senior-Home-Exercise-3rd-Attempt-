import { Component, output, signal } from '@angular/core';
import { ButtonComponent } from "../inputs/button/button.component";
import { DepartmentCardComponent } from "./department-card/department-card.component";
import { PaybandCardComponent } from "./payband-card/payband-card.component";
import { LookupService } from 'src/app/services/lookup.service';
import { Observable, of } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { Department } from 'src/app/models/department';
import { PayBand } from 'src/app/models/payband';
import { EditorAlertComponent } from "../editor-alert/editor-alert.component";
import { EditorAlertService } from 'src/app/services/editor-alert.service';
import { ErrorService } from 'src/app/services/error.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorBag } from 'src/app/models/errors/error-bag';
import { BaseViewModel } from 'src/app/models/base-view-model';
import { LookupItemsEnum } from 'src/app/models/lookup-items-enum';

enum AdminItems {
  Departments = 1,
  PayBands
}

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [ButtonComponent, DepartmentCardComponent, PaybandCardComponent, AsyncPipe, EditorAlertComponent],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.scss'
})
export class AdminPanelComponent {
  choice = signal<AdminItems | null>(null);

  $data = signal<Observable<any[]>>(of([]));

  newItems = signal<any[]>([]);

  exit = output();

  AdminItems = AdminItems;

  constructor(
    private readonly lookupService: LookupService,
    private readonly alertService: EditorAlertService,
    private readonly errorService: ErrorService
  ) {
  }

  changeChoice(item: AdminItems) {
    this.choice.set(item);
    this.loadData();
  }

  exitAdmin() {
    this.exit.emit();
  }

  loadData() {
    switch (this.choice()) {
      case AdminItems.Departments:
        this.$data.set(this.lookupService.getDepartmentsForEdit());
        break;
      case AdminItems.PayBands:
        this.$data.set(this.lookupService.getPayBandsForEdit());
        break;
      default:
        this.$data.set(of([]));
    }
    this.newItems.set([]);
  }

  addNew() {
    const arr = this.newItems();
    switch (this.choice()) {
      case AdminItems.Departments:
        arr.push({
          id: 0,
          name: "",
        } as Department);
        break;
      case AdminItems.PayBands:
        arr.push({
          id: 0,
          name: ""
        } as PayBand);
        break;
      default:
        return;
    }

    this.newItems.set(arr);
  }

  onSaveDepartment(bag: { item: Department, index: number }) {
    this.onSaveItem(
      bag.item,
      (item) => this.lookupService.addDepartment(item),
      (id, item) => this.lookupService.editDepartment(id, item),
      () => this.lookupService.getLookupItems(LookupItemsEnum.Department, true),
      "Department",
      bag.index
    );
  }

  onSavePayband(bag: { item: PayBand, index: number }) {
    this.onSaveItem(
      bag.item,
      (item) => this.lookupService.addPayBand(item),
      (id, item) => this.lookupService.editPayBand(id, item),
      () => this.lookupService.getLookupItems(LookupItemsEnum.PayBand, true),
      "Pay band",
      bag.index
    );
  }

  onSaveItem<T extends BaseViewModel>(item: T, addFunc: (item: T) => Observable<T>, editFunc: (id: number, item: T) => Observable<T>, refreshFunc: () => void, keyword: string, index: number) {
    const isNew = item.id === 0;
    let observable: Observable<T>;
    if (isNew) {
      observable = addFunc(item);
    } else {
      observable = editFunc(item.id!, item);
    }

    observable.subscribe({
      next: result => {
        this.alertService.sendAlert(`✔️ ${keyword} ${isNew ? 'added' : 'saved'} successfully!`);
        refreshFunc();
        if (isNew) this.loadData();
      },
      error: error => {
        const errorResp = error as HttpErrorResponse;
        const errorBag = errorResp.error as ErrorBag;
        errorBag.errors.map(x => x.propertyName = x.propertyName + item.id + index);
        this.errorService.displayErrors(errorBag);
        this.alertService.sendAlert(`❌ Unable to save ${keyword.toLowerCase()}`, true);
      }
    });
  }

  onDeleteItem(index: number) {
    this.newItems.set(this.newItems().filter((v, i) => i !== index));
  }
}
