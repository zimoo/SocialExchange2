using SocialExchange;

public class ImplicitRecognitionActivity
{
    protected SetOnceBackingField<PlayerInput> _PlayerInput;
    public PlayerInput PlayerInput 
    { 
        get
        {
            return _PlayerInput.Value;
        }
        set
        {
            _PlayerInput.Value = value;
        }
    }

    protected SetOnceBackingField<ImplicitRecognitionOutcome> _Outcome;
    public ImplicitRecognitionOutcome Outcome 
    { 
        get
        {
            return _Outcome.Value;
        }
    }

    public ImplicitRecognitionActivity()
    {
        _PlayerInput = new SetOnceBackingField<PlayerInput>(PlayerInputs.INDETERMINATE);
        _Outcome = new SetOnceBackingField<ImplicitRecognitionOutcome>(ImplicitRecognitionOutcomes.INDETERMINATE);

        _Outcome.Value = ImplicitRecognitionOutcomes.INDETERMINATE;
    }
}