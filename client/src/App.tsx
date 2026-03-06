import { useState } from 'react';
import { SubmissionForm, ResultDisplay } from './components';
import { config } from './utils/env';

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
        <div className="min-h-screen bg-slate-50 flex items-center justify-center p-4">
            <div className="w-full max-w-md bg-white rounded-2xl shadow-xl p-8 space-y-6">
                <header>
                    <h1 className="text-2xl font-bold text-slate-900">Currency Converter</h1>
                    <p className="text-slate-500 text-sm">
                        Convert numeric amounts to formal words.
                    </p>
                </header>
                <SubmissionForm
                    amount={amount}
                    setAmount={setAmount}
                    onConvert={handleConvert}
                    isLoading={isLoading}
                    error={error}
                />
                {result && <ResultDisplay result={result} />}
            </div>
        </div>
    );
}

export default App;
