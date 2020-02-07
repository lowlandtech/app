import { Component, OnInit, HostBinding, Input, Output, EventEmitter } from '@angular/core';
import { Card } from './card.model';

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
  @Output() hide: EventEmitter<Card>;
  @Output() remove: EventEmitter<Card>;
  @Output() collapse: EventEmitter<Card>;
  @Output() expand: EventEmitter<Card>;
  @Output() edit: EventEmitter<Card>;
  @Output() append: EventEmitter<Card>;
  @Output() prepend: EventEmitter<Card>;

  constructor() { }

  ngOnInit(): void {
  }

}
