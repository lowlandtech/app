import { AuthGuardService } from '@lowlandtech/auth';
import { NgrxFormsModule } from '@lowlandtech/ngrx-forms';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { EditorEffects } from './+state/editor.effects';
import { EditorFacade } from './+state/editor.facade';
import { editorReducer, editorInitialState } from './+state/editor.reducer';
import { CardEditorComponent } from './card-editor/card-editor.component';
import { EditorResolverService } from './editor-resolver.service';
import { EditorService } from './editor.service';

@NgModule({
  imports: [
    CommonModule,
    NgrxFormsModule,
    RouterModule.forChild([
      {
        path: '',
        pathMatch: 'full',
        component: CardEditorComponent,
        resolve: { EditorResolverService },
        canActivate: [AuthGuardService],
      },
      {
        path: ':slug',
        component: CardEditorComponent,
        resolve: { EditorResolverService },
      },
    ]),
    StoreModule.forFeature('editor', editorReducer, {
      initialState: editorInitialState,
    }),
    EffectsModule.forFeature([EditorEffects]),
  ],
  declarations: [CardEditorComponent],
  providers: [EditorEffects, EditorService, EditorResolverService, EditorFacade],
})
export class EditorModule {}
