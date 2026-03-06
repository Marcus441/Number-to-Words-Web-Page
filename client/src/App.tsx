import { useState } from 'react';
import { SubmissionForm, ResultDisplay } from './components';
import { config } from './utils/env';
import './App.css';

function App() {
    const [amount, setAmount] = useState('');
    const [result, setResult] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');

    const handleConvert = async () => {
        if (!amount) return;

        setIsLoading(true);
        setError('');

        try {
            // adjust port to match api port
            const response = await fetch(`${config.apiUrl}/api/convert?amount=${amount}`);

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Validation failed');
            }

            const data = await response.json();
            setResult(data.words);
        } catch (err: unknown) {
            if (err instanceof Error) {
                setError(err.message);
            } else {
                setError('An unexpected error occurred');
            }
            setResult('');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="app-wrapper">
            <main className="card">
                <header className="app-header">
                    <h1>Currency Converter</h1>
                    <p>Convert numeric amounts to formal words.</p>
                </header>
                <SubmissionForm
                    amount={amount}
                    setAmount={setAmount}
                    onConvert={handleConvert}
                    isLoading={isLoading}
                    error={error}
                />
                {result && <ResultDisplay result={result} />}
            </main>
        </div>
    );
}

export default App;
