import { Component, input, output } from '@angular/core';
import { Department } from 'src/app/models/department';
import { TextboxComponent } from "../../inputs/textbox/textbox.component";
import { ButtonComponent } from "../../inputs/button/button.component";
import { DatePipe } from '@angular/common';
import { ItemCardBase } from '../item-card-base';

@Component({
  selector: 'app-department-card',
  standalone: true,
  imports: [TextboxComponent, ButtonComponent, DatePipe],
  templateUrl: './department-card.component.html',
  styleUrl: './department-card.component.scss'
})
export class DepartmentCardComponent extends ItemCardBase<Department> {
}
