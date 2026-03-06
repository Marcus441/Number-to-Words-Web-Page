import React from 'react';
import './SubmissionForm.css';

interface SubmissionFormProps {
    amount: string;
    setAmount: (value: string) => void;
    onConvert: () => void;
    isLoading: boolean;
    error?: string;
}

export const SubmissionForm: React.FC<SubmissionFormProps> = ({
    amount,
    setAmount,
    onConvert,
    isLoading,
    error,
}) => {
    return (
        <div className="converter-form">
            <div className="input-wrapper">
                <span className="currency-symbol">$</span>
                <input
                    type="text"
                    value={amount}
                    onChange={(e) => setAmount(e.target.value)}
                    className={`amount-input ${error ? 'input-error' : ''}`}
                    placeholder="0.00"
                />
                {error && <p className="error-message">{error}</p>}
            </div>

            <button onClick={onConvert} disabled={isLoading || !amount} className="btn-convert">
                {isLoading ? 'Converting...' : 'Convert to Words'}
            </button>
        </div>
    );
};
