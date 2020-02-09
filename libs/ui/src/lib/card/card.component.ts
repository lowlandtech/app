/* #region imports */
import {
  Component,
  OnInit,
  HostBinding,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef
} from '@angular/core';
import { Card } from './card.model';
/* #endregion */

@Component({
  selector: 'scx-ui-card, [component="scx-ui-card"]',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @Input() card: Card;
  @Input() hasHeader = true;
  @Input() hasSettings = false;
  @Input() sizeable = true;
  @Input() closeable = true;
  @Input() hasFooter = false;
  @Input() cancelable = true;
  @Input() okable = true;

  @Output() hide: EventEmitter<Card> = new EventEmitter();;
  @Output() remove: EventEmitter<Card> = new EventEmitter();;
  @Output() collapse: EventEmitter<Card> = new EventEmitter();;
  @Output() expand: EventEmitter<Card> = new EventEmitter();;
  @Output() edit: EventEmitter<Card> = new EventEmitter();;
  @Output() append: EventEmitter<Card> = new EventEmitter();;
  @Output() prepend: EventEmitter<Card> = new EventEmitter();;

  constructor(private element: ElementRef) {}

  ngOnInit(): void {

  }
}
