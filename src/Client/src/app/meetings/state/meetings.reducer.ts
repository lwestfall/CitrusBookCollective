import { createReducer, on } from '@ngrx/store';
import { MeetingDto } from '../../api/models';
import * as meetingsActions from './meetings.actions';

export interface MeetingsState {
  allMeetingStates: MeetingState[];
  isLoadingAllMeetings: boolean;
  allMeetingsError: string | null;
}

export interface MeetingState {
  meeting: MeetingDto;
  error: string | null;
}

export const initialState: MeetingsState = {
  allMeetingStates: [],
  isLoadingAllMeetings: false,
  allMeetingsError: null,
};

export const meetingsReducer = createReducer(
  initialState,
  on(
    meetingsActions.getAllMeetings,
    (state): MeetingsState => ({
      ...state,
      isLoadingAllMeetings: true,
      allMeetingsError: null,
    })
  ),
  on(
    meetingsActions.getAllMeetingsSuccess,
    (state, action): MeetingsState => ({
      ...state,
      allMeetingStates: action.meetings.map(meeting => ({
        meeting,
        isLoading: false,
        error: null,
      })),
      isLoadingAllMeetings: false,
    })
  ),
  on(
    meetingsActions.getAllMeetingsFailure,
    (state, action): MeetingsState => ({
      ...state,
      isLoadingAllMeetings: false,
      allMeetingsError: action.error,
    })
  ),
  on(
    meetingsActions.handleMeetingUpdate,
    (state, action): MeetingsState => ({
      ...state,
      allMeetingStates: state.allMeetingStates.map(meetingState =>
        meetingState.meeting.id === action.meeting.id
          ? { meeting: action.meeting, error: null }
          : meetingState
      ),
    })
  ),
  on(
    meetingsActions.handleMeetingError,
    (state, action): MeetingsState => ({
      ...state,
      allMeetingStates: state.allMeetingStates.map(meetingState =>
        meetingState.meeting.id === action.meetingId
          ? { meeting: meetingState.meeting, error: action.error }
          : meetingState
      ),
    })
  )
);
