import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { HomeFacade } from '../+state/home.facade';
import { Observable } from 'rxjs';

@Component({
  selector: 'scx-tags-list',
  template: `
    <p>Popular Tags</p>

    <div class="tag-list">
      <a *ngFor="let tag of tags$|async" (click)="setListTag(tag)" class="tag-pill tag-default">{{ tag }}</a>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TagsListComponent implements OnInit {
  tags$: Observable<string[]>;
  constructor(private facade: HomeFacade){  }

  ngOnInit() {
    this.tags$ = this.facade.tags$;
  }

  public setListTag(tag: string){
    this.facade.selectTag(tag);
  }
}
