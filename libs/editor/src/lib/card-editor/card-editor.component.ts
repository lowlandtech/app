import { Field, NgrxFormsFacade } from '@lowlandtech/ngrx-forms';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { EditorFacade } from '../+state/editor.facade';

const structure: Field[] = [
  {
    type: 'INPUT',
    name: 'title',
    placeholder: 'Card Title',
    validator: [Validators.required],
  },
  {
    type: 'INPUT',
    name: 'description',
    placeholder: "What's this card about?",
    validator: [Validators.required],
  },
  {
    type: 'TEXTAREA',
    name: 'body',
    placeholder: 'Write your card (in markdown)',
    validator: [Validators.required],
  },
  {
    type: 'INPUT',
    name: 'tagList',
    placeholder: 'Enter Tags',
    validator: [],
  },
];

@Component({
  selector: 'scx-card-editor',
  templateUrl: './card-editor.component.html',
  styleUrls: ['./card-editor.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardEditorComponent implements OnInit, OnDestroy {
  structure$: Observable<Field[]>;
  data$: Observable<any>;

  constructor(private ngrxFormsFacade: NgrxFormsFacade, private router: Router, private facade: EditorFacade) {}

  ngOnInit() {
    this.ngrxFormsFacade.setStructure(structure);
    this.data$ = this.ngrxFormsFacade.data$;
    this.structure$ = this.ngrxFormsFacade.structure$;
    this.facade.card$.subscribe(card => this.ngrxFormsFacade.setData(card));
  }

  updateForm(changes: any) {
    this.ngrxFormsFacade.updateData(changes);
  }

  submit() {
    this.facade.publishCard();
  }

  ngOnDestroy() {
    this.ngrxFormsFacade.initializeForm();
    this.facade.initializeCard();
  }
}
