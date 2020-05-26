import { NgrxFormsModule } from '@lowlandtech/ngrx-forms';
import { SharedModule } from '@lowlandtech/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { CardEffects } from './+state';
import { CardFacade } from './+state';
import { cardInitialState, cardReducer } from './+state';
import { AddCommentComponent } from './add-comment';
import { CardCommentComponent } from './card-comment';
import { CardGuardService } from './services';
import { CardMetaComponent } from './card-meta';
import { CardComponent } from './card';
import { CardService } from './services';
import { MarkdownPipe } from './pipes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: CardComponent,
        canActivate: [CardGuardService]
      }
    ]),
    StoreModule.forFeature('card', cardReducer, {
      initialState: cardInitialState
    }),
    EffectsModule.forFeature([CardEffects]),
    NgrxFormsModule,
    SharedModule
  ],
  providers: [
    CardEffects,
    CardService,
    CardGuardService,
    CardFacade
  ],
  declarations: [
    CardComponent,
    CardMetaComponent,
    CardCommentComponent,
    MarkdownPipe,
    AddCommentComponent
  ]
})
export class CardModule { }
