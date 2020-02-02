import { Entity, AsideState } from './aside.reducer';
import { asideQuery } from './aside.selectors';

describe('Aside Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getAsideId = it => it['id'];

  let storeState;

  beforeEach(() => {
    const createAside = (id: string, name = ''): Entity => ({
      id,
      name: name || `name-${id}`
    });
    storeState = {
      aside: {
        list: [
          createAside('PRODUCT-AAA'),
          createAside('PRODUCT-BBB'),
          createAside('PRODUCT-CCC')
        ],
        selectedId: 'PRODUCT-BBB',
        error: ERROR_MSG,
        loaded: true
      }
    };
  });

  describe('Aside Selectors', () => {
    it('getAllAside() should return the list of Aside', () => {
      const results = asideQuery.getAllAside(storeState);
      const selId = getAsideId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelectedAside() should return the selected Entity', () => {
      const result = asideQuery.getSelectedAside(storeState);
      const selId = getAsideId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it("getLoaded() should return the current 'loaded' status", () => {
      const result = asideQuery.getLoaded(storeState);

      expect(result).toBe(true);
    });

    it("getError() should return the current 'error' storeState", () => {
      const result = asideQuery.getError(storeState);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
