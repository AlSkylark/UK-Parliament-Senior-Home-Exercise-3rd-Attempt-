import { Component, input, output } from "@angular/core";
import { BaseViewModel } from "src/app/models/base-view-model";

@Component({ template: '' })
export class ItemCardBase<T extends BaseViewModel> {
    item = input.required<T>();
    index = input.required<number>();

    saveItem = output<{ item: T, index: number }>();
    deleteItem = output<number>();

    onSave() {
        this.saveItem.emit({ item: this.item(), index: this.index() });
    }

    onDelete() {
        this.deleteItem.emit(this.index())
    }
}