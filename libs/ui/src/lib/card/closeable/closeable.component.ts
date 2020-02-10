import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Card } from '../card.model';

@Component({
  selector: 'scx-ui-closeable',
  templateUrl: './closeable.component.html',
  styleUrls: ['./closeable.component.scss']
})
export class CloseableComponent implements OnInit {
  @Input() card: Card;
  @Output() closing: EventEmitter<Card> = new EventEmitter();
  @Output() cancelling: EventEmitter<Card> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
