import { async, TestBed } from '@angular/core/testing';
import { NgrxFormsModule } from './ngrx-forms.module';

describe('NgrxFormsModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [NgrxFormsModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(NgrxFormsModule).toBeDefined();
  });
});
