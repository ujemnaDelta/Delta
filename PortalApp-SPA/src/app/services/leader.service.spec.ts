/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LeaderService } from './leader.service';

describe('Service: Leader', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaderService]
    });
  });

  it('should ...', inject([LeaderService], (service: LeaderService) => {
    expect(service).toBeTruthy();
  }));
});
