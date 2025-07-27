import { Component, input } from '@angular/core';
import { PayBand } from 'src/app/models/payband';
import { TextboxComponent } from "../../inputs/textbox/textbox.component";
import { ButtonComponent } from "../../inputs/button/button.component";
import { NumberComponent } from "../../inputs/number/number.component";
import { ItemCardBase } from '../item-card-base';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-payband-card',
  standalone: true,
  imports: [TextboxComponent, ButtonComponent, NumberComponent, DatePipe],
  templateUrl: './payband-card.component.html',
  styleUrl: './payband-card.component.scss'
})
export class PaybandCardComponent extends ItemCardBase<PayBand> {
}
