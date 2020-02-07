/* #region imports */
import {
  Component,
  OnInit,
  HostBinding,
  Input,
  Output,
  EventEmitter
} from '@angular/core';
import { Card } from './card.model';
/* #endregion */

@Component({
  selector: 'scx-ui-card',
  template: `
    <ng-content></ng-content>
  `,
  styles: [`
    :host {
      margin-bottom: 10px;
    }
  `]
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @Input() card: Card;
  @Output() hide: EventEmitter<Card> = new EventEmitter();;
  @Output() remove: EventEmitter<Card> = new EventEmitter();;
  @Output() collapse: EventEmitter<Card> = new EventEmitter();;
  @Output() expand: EventEmitter<Card> = new EventEmitter();;
  @Output() edit: EventEmitter<Card> = new EventEmitter();;
  @Output() append: EventEmitter<Card> = new EventEmitter();;
  @Output() prepend: EventEmitter<Card> = new EventEmitter();;

  constructor() {}

  ngOnInit(): void {}
}
