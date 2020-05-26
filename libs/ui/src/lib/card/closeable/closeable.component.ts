import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'scx-ui-closeable',
  templateUrl: './closeable.component.html',
  styleUrls: ['./closeable.component.scss']
})
export class CloseableComponent implements OnInit {
  @Output() closing: EventEmitter<string> = new EventEmitter();
  @Output() cancelling: EventEmitter<string> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}
}
