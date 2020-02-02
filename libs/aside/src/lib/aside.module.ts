import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AsideComponent } from './aside.component';
import { StoreModule, ActionReducer, MetaReducer } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromAside from './+state/aside.reducer';
import { AsideEffects } from './+state/aside.effects';
import { AsideFacade } from './+state/aside.facade';
import { NxModule } from '@nrwl/angular';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '@env/environment';
import { AsideTogglerDirective } from './aside.toggler';
import { localStorageSync } from 'ngrx-store-localstorage';

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return localStorageSync({keys: [fromAside.ASIDE_FEATURE_KEY], rehydrate: true})(reducer);
}
const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer];

@NgModule({
  imports: [
    CommonModule,
    NxModule.forRoot(),
    StoreModule.forRoot(
      {},
      {
        metaReducers: !environment.production ? metaReducers : metaReducers,
        runtimeChecks: {
          strictActionImmutability: true,
          strictStateImmutability: true
        }
      }
    ),
    EffectsModule.forRoot([AsideEffects]),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    StoreModule.forFeature(fromAside.ASIDE_FEATURE_KEY, fromAside.reducer)
  ],
  declarations: [AsideComponent,AsideTogglerDirective],
  exports: [AsideComponent, AsideTogglerDirective],
  providers: [AsideFacade]
})
export class AsideModule {}
