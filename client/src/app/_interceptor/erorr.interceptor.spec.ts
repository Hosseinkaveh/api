import { TestBed } from '@angular/core/testing';

import { ErorrInterceptor } from './erorr.interceptor';

describe('ErorrInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      ErorrInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: ErorrInterceptor = TestBed.inject(ErorrInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
