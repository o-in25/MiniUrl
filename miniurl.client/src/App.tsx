import {useEffect, useState} from 'react';
import './App.css';
import {
	Alert,
	Button,
	Label,
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeadCell,
	TableRow,
	TextInput,
} from 'flowbite-react';
import {deleteCondensedUrl, getCondensedUrl, list} from './services/api';

// TODO:
// we could enable cors on the short url route
// and do a 'redirect: 'follow'.
// for time sake, I am just hardcoding the port
// we could also add this as an env variable
const HOSTNAME = 'https://localhost:7094';

interface ShortUrl {
	longUrl: string;
	customShortUrl?: string | null;
	clickCount: number;
	createdAt: string;
	hashKey: string;
}

// TODO: we have this duplicated in the config,
// move this to a shared export

const App = () => {
	const [rows, setRows] = useState<ShortUrl[]>([]);
	const [formData, setFormData] = useState<Partial<ShortUrl>>({
		longUrl: '',
		customShortUrl: '',
	});

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const {name, value} = e.target;
		console.log(name);
		setFormData(prevData => ({
			...prevData,
			[name]: value,
		}));
	};

	const populateTable = async () => {
		const rows = await list();
		setRows(rows);
	};

	const addUrl = async () => {
		if (!formData.longUrl) return; // TODO: throw an error here
		await getCondensedUrl(formData.longUrl, formData.customShortUrl);
		await populateTable();
	};

	const deleteUrl = async (hashKey: string) => {
		await deleteCondensedUrl(hashKey);
		await populateTable();
	};

	useEffect(() => {
		populateTable();
	}, []);
	return (
		<div className="mx-auto p-4">
			<h1 className="mb-4 text-lg font-extrabold leading-none tracking-tight text-gray-900 md:text-5xl lg:text-6xl">
				MiniURL
			</h1>
			<p className="mb-6 text-lg font-normal text-gray-500 lg:text-xl sm:px-16 xl:px-48 ">
				URL shortening service with support for creating and deleting short URLs
				from long ones.
			</p>

			<form className="flex flex-col gap-4">
				<div className="grid gap-4 mb-4 md:grid-cols-3">
					<div>
						<div className="mb-2 block text-left">
							<Label htmlFor="longUrl">Long URL</Label>
						</div>
						<TextInput
							value={formData.longUrl}
							onChange={handleChange}
							name="longUrl"
							id="longUrl"
							type="text"
							placeholder="somereallylongurl.com"
							required
						/>
					</div>
					<div>
						<div className="mb-2 block text-left">
							<Label htmlFor="customShortUrl">Short URL (Optional)</Label>
						</div>
						<TextInput
							value={formData?.customShortUrl || ''}
							onChange={handleChange}
							name="customShortUrl"
							id="customShortUrl"
							type="text"
							placeholder="shorturl.com"
							required
							shadow
						/>
					</div>
					<Button
						type="button"
						className="mx-auto my-8 w-full"
						onClick={addUrl}>
						Add
					</Button>
				</div>
			</form>

			{/* TODO: move the table to its own component if i have time */}
			<div className="overflow-x-auto">
				<Table>
					<TableHead>
						<TableRow>
							<TableHeadCell>Long URL</TableHeadCell>
							<TableHeadCell>Short URL</TableHeadCell>
							<TableHeadCell>Clicked Count</TableHeadCell>
							<TableHeadCell>
								<span className="sr-only">Edit</span>
							</TableHeadCell>
						</TableRow>
					</TableHead>
					<TableBody className="divide-y">
						{rows.map(({longUrl, clickCount, hashKey}, index) => (
							<TableRow
								className="bg-white"
								key={index}>
								<TableCell className="whitespace-nowrap font-medium text-gray-900 ">
									<a>{longUrl}</a>
								</TableCell>
								<TableCell>
									<a
										className="hover:underline"
										href={`${HOSTNAME}/${hashKey}`}>
										{hashKey}
									</a>
								</TableCell>
								<TableCell>{clickCount}</TableCell>
								<TableCell>
									<Button
										type="button"
										className="mx-auto float-right"
										color={'red'}
										onClick={() => deleteUrl(hashKey)}>
										Delete
									</Button>
								</TableCell>
							</TableRow>
						))}
					</TableBody>
				</Table>
				{rows.length === 0 && (
					<div className="py-4 w-1/2 mx-auto">
						<Alert color="info">
							<span className="font-medium">No results found.</span> But you can
							change that!
						</Alert>
					</div>
				)}
			</div>
		</div>
	);
};

export default App;
