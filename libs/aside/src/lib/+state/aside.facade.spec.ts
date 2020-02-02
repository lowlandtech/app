import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { readFirst } from '@nrwl/angular/testing';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule, Store } from '@ngrx/store';

import { NxModule } from '@nrwl/angular';

import { AsideEffects } from './aside.effects';
import { AsideFacade } from './aside.facade';

import { asideQuery } from './aside.selectors';
import { LoadAside, AsideLoaded } from './aside.actions';
import { AsideState, Entity, initialState, reducer } from './aside.reducer';

interface TestSchema {
  aside: AsideState;
}

describe('AsideFacade', () => {
  let facade: AsideFacade;
  let store: Store<TestSchema>;
  let createAside;

  beforeEach(() => {
    createAside = (id: string, name = ''): Entity => ({
      id,
      name: name || `name-${id}`
    });
  });

  describe('used in NgModule', () => {
    beforeEach(() => {
      @NgModule({
        imports: [
          StoreModule.forFeature('aside', reducer, { initialState }),
          EffectsModule.forFeature([AsideEffects])
        ],
        providers: [AsideFacade]
      })
      class CustomFeatureModule {}

      @NgModule({
        imports: [
          NxModule.forRoot(),
          StoreModule.forRoot({}),
          EffectsModule.forRoot([]),
          CustomFeatureModule
        ]
      })
      class RootModule {}
      TestBed.configureTestingModule({ imports: [RootModule] });

      store = TestBed.get(Store);
      facade = TestBed.get(AsideFacade);
    });

    /**
     * The initially generated facade::loadAll() returns empty array
     */
    it('loadAll() should return empty list with loaded == true', async done => {
      try {
        let list = await readFirst(facade.allAside$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        facade.loadAll();

        list = await readFirst(facade.allAside$);
        isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });

    /**
     * Use `AsideLoaded` to manually submit list for state management
     */
    it('allAside$ should return the loaded list; and loaded flag == true', async done => {
      try {
        let list = await readFirst(facade.allAside$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        store.dispatch(
          new AsideLoaded([createAside('AAA'), createAside('BBB')])
        );

        list = await readFirst(facade.allAside$);
        isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(2);
        expect(isLoaded).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });
  });
});
