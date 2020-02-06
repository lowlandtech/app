import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'scx-tags-list',
  template: `
    <p>Popular Tags</p>

    <div class="tag-list">
      <a *ngFor="let tag of tags" (click)="setListTag.emit(tag)" class="tag-pill tag-default">{{ tag }}</a>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TagsListComponent {
  @Input() tags: string[];
  @Output() setListTag: EventEmitter<string> = new EventEmitter();
}
