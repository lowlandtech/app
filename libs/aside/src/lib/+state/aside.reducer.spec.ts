import { AsideLoaded } from './aside.actions';
import { AsideState, Entity, initialState, reducer } from './aside.reducer';

describe('Aside Reducer', () => {
  const getAsideId = it => it['id'];
  let createAside;

  beforeEach(() => {
    createAside = (id: string, name = ''): Entity => ({
      id,
      name: name || `name-${id}`
    });
  });

  describe('valid Aside actions ', () => {
    it('should return set the list of known Aside', () => {
      const asides = [createAside('PRODUCT-AAA'), createAside('PRODUCT-zzz')];
      const action = new AsideLoaded(asides);
      const result: AsideState = reducer(initialState, action);
      const selId: string = getAsideId(result.list[1]);

      expect(result.loaded).toBe(true);
      expect(result.list.length).toBe(2);
      expect(selId).toBe('PRODUCT-zzz');
    });
  });

  describe('unknown action', () => {
    it('should return the previous state', () => {
      const action = {} as any;
      const result = reducer(initialState, action);

      expect(result).toBe(initialState);
    });
  });
});
